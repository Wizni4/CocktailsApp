// auth.service.ts
export interface IAuthService {
  getUser(): Promise<any>;
  getJwtToken(): Promise<string>
  updateUser(user: IUser): Promise<any>;
  signUp(user: IUser): Promise<any>;
  signIn(user: IUser): Promise<any>;
  signOut(): Promise<any>;
  confirmSignUp(user: IUser): Promise<any>;
  isAuthenticated(): Promise<boolean>;
}

export interface IUser {
  name: string;
  email: string;
  password: string;
  showPassword: boolean;
  code: string;
}
