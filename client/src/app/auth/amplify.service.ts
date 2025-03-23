import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Amplify } from 'aws-amplify';
import {
  confirmSignUp,
  getCurrentUser,
  fetchUserAttributes,
  fetchAuthSession,
  signIn,
  signUp,
  signOut,
  updateUserAttribute,
} from 'aws-amplify/auth'
import { environment } from '../../environments/environment';
import { IAuthService, IUser } from './auth.service';



@Injectable({
  providedIn: 'root',
})
export class AmplifyService implements IAuthService {

  private authenticationSubject: BehaviorSubject<any>;

  constructor() {
    Amplify.configure({
      Auth: environment.Auth
    });

    this.authenticationSubject = new BehaviorSubject<boolean>(false);
  }

  public signUp(user: IUser): Promise<any> {
    return signUp({
      username: user.name,
      password: user.password,
      options: {
        userAttributes: {
          email: user.email
        }
      }
    });
  }

  public confirmSignUp(user: IUser): Promise<any> {
    return confirmSignUp({
      username: user.name,
      confirmationCode: user.code
    });
  }

  public signIn(user: IUser): Promise<any> {
    return signIn({
      username: user.name,
      password: user.password
    }).then(() => {
      this.authenticationSubject.next(true);
    });
  }

  public signOut(): Promise<any> {
    return signOut()
      .then(() => {
        this.authenticationSubject.next(false);
      });
  }

  public isAuthenticated(): Promise<boolean> {
    if (this.authenticationSubject.value) {
      return Promise.resolve(true);
    } else {
      return getCurrentUser()
        .then((user: any) => {
          if (user) {
            return true;
          } else {
            return false;
          }
        }).catch(() => {
          return false;
        });
    }
  }

  public async getJwtToken(): Promise<string> {
    try {
      const session = await fetchAuthSession();
      const idToken = session.tokens?.idToken?.toString();

      if (!idToken) {
        throw new Error('ID token is missing from the session');
      }

      return idToken;
    } catch (error) {
      console.error('Error fetching JWT token:', error);
      throw error;
    }
  }

  public getUser(): Promise<any> {
    return fetchUserAttributes();
  }

  public updateUser(user: IUser): Promise<any> {
    return fetchUserAttributes()
      .then((cognitoUser: any) => {
        return updateUserAttribute({
          userAttribute: cognitoUser
        });
      });
  }

}
