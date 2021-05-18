import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';
import { Router } from '@angular/router';

@Component({ templateUrl: 'users.component.html' })
export class UsersComponent {
    loading = false;
    users: User[];
    isAdminUser: boolean = false;
    currentUser: any = {
        userName: ''
    };
    constructor(private userService: UserService, private router: Router, private authenticationService: AuthenticationService) { }

    ngOnInit() {
        this.currentUser = this.authenticationService.currentUserValue;
        this.loadUsers();
    }

    loadUsers() {
        this.loading = true;
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.loading = false;
            this.users = users;
            this.users.forEach(u => {
                if (u.roles && u.roles.length > 0) {
                    u['rolePlain'] = u.roles.map(s => s.roleName); 
                }
                if (this.currentUser.id === u.userId) {
                    if (u['rolePlain'].includes('Global Gleason Admin')) {
                        this.isAdminUser = true;
                    }
                }
            });
        });
    }

    edit(user) {
        localStorage.setItem('userToUpdate', JSON.stringify(user));
        this.router.navigateByUrl('/addOrUpdateuser?act=update');
    }

    delete(user) {
        this.userService.deleteUser(user).pipe(first()).subscribe(resp => {
            this.loadUsers();
        });
    }
}