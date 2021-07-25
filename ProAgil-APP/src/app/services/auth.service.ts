import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'https://localhost:44343/api/user';
  jwtHelpers = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  Login(model: any) {
    return this.http.post(`${this.baseURL}/login`, model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token); // Apenas a mesma URL consegue acessar o localStorage.
          this.decodedToken = this.jwtHelpers.decodeToken(user.token);
        }
      })
    );
  }

  Register(model: any) {
    return this.http.post(`${this.baseURL}/register`, model);
  }

  LoggedIn() {
    const token = localStorage.getItem('token');

    if (token === null) {
      return false;
    }

    return !this.jwtHelpers.isTokenExpired(token!);
  }
}
