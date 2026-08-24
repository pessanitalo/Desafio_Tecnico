import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../models/login';
import { environment } from '../../../../environments/environment.prod';


@Injectable({
    providedIn: 'root'
})
export class LoginService {
    baseUrl = `${environment.mainUrlAPI}login`;
    constructor(private http: HttpClient) { }

    login(login: Login) {
        return this.http.post<Login>(this.baseUrl, login);
    }
}
