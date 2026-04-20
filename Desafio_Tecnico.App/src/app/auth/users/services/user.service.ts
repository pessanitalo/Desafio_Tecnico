import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.prod';
import { User } from '../models/user';


@Injectable({
    providedIn: 'root'
})

export class UserService {
    baseUrl = `${environment.mainUrlAPI}user`;

    constructor(private http: HttpClient) { }

    adicionar(user: User) {
        return this.http.post<User>(this.baseUrl, user);
    }
}
