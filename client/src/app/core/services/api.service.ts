import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

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

  private buildRequestOptions(): {
    headers: HttpHeaders;
    params: HttpParams;
  } {
    const headers = new HttpHeaders({
      ...this.headers,
      Accept: 'application/json',
    });
    const params = new HttpParams({ fromObject: this.queryParams });
    return { headers, params };
  }

  private resetState(): void {
    this.body = {};
    this.headers = {};
    this.queryParams = {};
  }

  public get<T>(endpoint: string): Observable<T> {
    const options = this.buildRequestOptions();
    const obs = this.http.get<T>(`${this.apiUrl}/${endpoint}`, {
      headers: options.headers,
      params: options.params,
      withCredentials: true,
    });
    this.resetState();
    return obs;
  }

  public post<T>(endpoint: string): Observable<T> {
    const options = this.buildRequestOptions();
    const obs = this.http.post<T>(`${this.apiUrl}/${endpoint}`, this.body, {
      headers: options.headers,
      params: options.params,
      withCredentials: true,
    });
    this.resetState();
    return obs;
  }

  public put<T>(endpoint: string): Observable<T> {
    const options = this.buildRequestOptions();
    const obs = this.http.put<T>(`${this.apiUrl}/${endpoint}`, this.body, {
      headers: options.headers,
      params: options.params,
      withCredentials: true,
    });
    this.resetState();
    return obs;
  }

  public delete<T>(endpoint: string): Observable<T> {
    const options = this.buildRequestOptions();
    const obs = this.http.delete<T>(`${this.apiUrl}/${endpoint}`, {
      headers: options.headers,
      params: options.params,
      withCredentials: true,
    });
    this.resetState();
    return obs;
  }
}
