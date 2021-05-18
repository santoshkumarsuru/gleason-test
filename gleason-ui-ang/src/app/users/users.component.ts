import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';

@Component({ templateUrl: 'users.component.html' })
export class UsersComponent {
    loading = false;
    users: User[];

    constructor(private userService: UserService) { }

    ngOnInit() {
        this.loading = true;
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.loading = false;
            this.users = users;
            this.users.forEach(u => {
                if (u.roles && u.roles.length > 0) {
                    u['rolePlain'] = u.roles.map(s => s.roleName); 
                }
            });
        });
    }
}