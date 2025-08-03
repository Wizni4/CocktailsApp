import { Observable } from 'rxjs';

export interface SignInRequest {
  username: string;
  password: string;
}

export interface SignUpRequest {
  email: string;
  username: string;
  password: string;
}

export interface AuthResult {
  accessToken: string;
  idtoken: string,
  refreshToken?: string;
}

