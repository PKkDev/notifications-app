import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiService } from './api.service';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

export interface LoginHttpResponse {
  id: number | null;
  token: string | null;
  role: string | null;
  name: string | null;
}

export class LoginDto {
  constructor(
    public username: string,
    public password: string) { }
}

export class RegisterDto {
  constructor(
    public username: string,
    public password: string,
    public email: string) { }
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public user: BehaviorSubject<LoginHttpResponse>;

  constructor(
    private route: Router,
    private apiService: ApiService) {

    const token = sessionStorage.getItem('token');
    const id = sessionStorage.getItem('id');
    const role = sessionStorage.getItem('role');
    const name = sessionStorage.getItem('name');

    if (token && id)
      this.user = new BehaviorSubject<LoginHttpResponse>({ token: token, id: +id, role: role, name: name });
    else
      this.user = new BehaviorSubject<LoginHttpResponse>({ token: null, id: null, role: null, name: null });

  }

  public logIn(login: string, pass: string): Observable<LoginHttpResponse> {
    const httpBody = new LoginDto(login, pass);
    return this.apiService.post<LoginHttpResponse>('auth/login', httpBody)
      .pipe(
        map((data: LoginHttpResponse) => {
          if (data) {
            if (data.token && data.id && data.role) {
              this.user.next({ token: data.token, id: data.id, role: data.role, name: data.name });
              sessionStorage.setItem('token', data.token);
              sessionStorage.setItem('id', data.id.toString());
              sessionStorage.setItem('role', data.role);
              sessionStorage.setItem('name', data.name);
            }
          }
          return data;
        })
      );
  }

  public register(login: string, pass: string, email: string): Observable<LoginHttpResponse> {
    const httpBody = new RegisterDto(login, pass, email);
    return this.apiService.post<LoginHttpResponse>('auth/register', httpBody)
      .pipe(
        map((data: LoginHttpResponse) => {
          if (data) {
            if (data.token && data.id && data.role) {
              this.user.next({ token: data.token, id: data.id, role: data.role, name: data.name });
              sessionStorage.setItem('token', data.token);
              sessionStorage.setItem('id', data.id.toString());
              sessionStorage.setItem('role', data.role);
              sessionStorage.setItem('name', data.name);
            }
          }
          return data;
        })
      );
  }

  public getUserName(): string | null {
    return this.user.getValue().name;
  }

  public logOut() {
    this.user = new BehaviorSubject<LoginHttpResponse>({ token: null, id: null, role: null, name: null });
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('id');
    sessionStorage.removeItem('role');
    this.route.navigate(['/']);
  }

  public getAuthorizationToken(): string | null {
    return this.user.getValue().token;
  }

  public checkLogIn(): boolean {
    return !!this.user.getValue().token;
  }

  public getUserId(): number {
    const id = sessionStorage.getItem('id');
    if (id)
      return +id
    return -1;
  }

}
