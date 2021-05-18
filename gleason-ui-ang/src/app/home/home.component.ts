﻿import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    currentUser: User;
    constructor(private userService: UserService, private authenticationService: AuthenticationService) { }

    ngOnInit() {
        this.currentUser = this.authenticationService.currentUserValue;
    }
}