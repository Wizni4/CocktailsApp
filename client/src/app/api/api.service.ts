import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = environment.ApiUrl;
  private body: { [key: string]: any } = {};
  private headers: { [key: string]: string } = {};
  private queryParams: { [key: string]: string } = {};

  constructor(private http: HttpClient) { }

  public withHeaders(headers: { [key: string]: string }): ApiService {
    this.headers = { ...this.headers, ...headers };
    return this;
  }

  public withQueryParam(params: { [key: string]: string }): ApiService {
    this.queryParams = { ...this.queryParams, ...params };
    return this;
  }

  public withBody(body: { [key: string]: any }): ApiService {
    this.body = body;
    return this;
  }

  private async buildRequestOptions(): Promise<{
    headers: HttpHeaders;
    params: HttpParams;
  }> {
    const headers = new HttpHeaders({
      ...this.headers,
      'Accept': 'application/json',
    });
    const params = new HttpParams({ fromObject: this.queryParams });
    return { headers, params };
  }

  private resetState(): void {
    this.body = {};
    this.headers = {};
    this.queryParams = {};
  }

  public async get(endpoint: string): Promise<object> {
    const options = await this.buildRequestOptions();
    const observable = this.http.get(`${this.apiUrl}/${endpoint}`, {
      headers: options.headers,
      params: options.params,
      withCredentials: true, // ⬅️ Important for HttpOnly cookie support
    });
    this.resetState();
    return await firstValueFrom(observable);
  }

  public async post(endpoint: string): Promise<object> {
    const options = await this.buildRequestOptions();
    const observable = this.http.post(`${this.apiUrl}/${endpoint}`, this.body, {
      headers: options.headers,
      params: options.params,
      withCredentials: true,
    });
    this.resetState();
    return await firstValueFrom(observable);
  }

  public async put(endpoint: string): Promise<object> {
    const options = await this.buildRequestOptions();
    const observable = this.http.put(`${this.apiUrl}/${endpoint}`, this.body, {
      headers: options.headers,
      params: options.params,
      withCredentials: true,
    });
    this.resetState();
    return await firstValueFrom(observable);
  }

  public async delete(endpoint: string): Promise<object> {
    const options = await this.buildRequestOptions();
    const observable = this.http.delete(`${this.apiUrl}/${endpoint}`, {
      headers: options.headers,
      params: options.params,
      withCredentials: true,
    });
    this.resetState();
    return await firstValueFrom(observable);
  }
}
