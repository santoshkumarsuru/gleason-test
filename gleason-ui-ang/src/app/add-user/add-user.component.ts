import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({ templateUrl: 'add-user.component.html' })
export class AddUserComponent {
    loading = false;
    users: User[];
    public currentUserAdded: User = {
        userId: 0,
        username: '',
        password: '',
        firstName: '',
        lastName: '',
        email: '',
        roles: [],
        customer: '',
        isTrailUser: false
    };
    checkedRoleList: any;
    isMasterSel:boolean = false;
    roleList = [
        {id:1, roleName:'Global Gleason Admin', isSelected:false},
        {id:2, roleName:'User', isSelected:true},
        {id:3, roleName:'Customer Admin', isSelected:false},
        {id:4, roleName:'Gleason Regional Sales Manager(RSM)', isSelected:false},
        {id:5, roleName:'Gleason Internal Sales', isSelected:false},
        {id:6, roleName:'Gleason Engineer / Service Engineer', isSelected:false},
    ];
    constructor(private userService: UserService, private router: Router) { }

    ngOnInit() {
        this.loading = true;
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.loading = false;
            this.users = users;
        });
        this.getCheckedItemList();
    }

    checkUncheckAll() {
        for (var i = 0; i < this.roleList.length; i++) {
            this.roleList[i].isSelected = this.isMasterSel;
        }
        this.getCheckedItemList();
    }
    
    isAllSelected() {
        this.isMasterSel = this.roleList.every(function(item:any) {
            return item.isSelected == true;
        })
        this.getCheckedItemList();
    }
    
    getCheckedItemList(){
        this.checkedRoleList = [];
        for (var i = 0; i < this.roleList.length; i++) {
        if(this.roleList[i].isSelected)       
            this.checkedRoleList.push(this.roleList[i]);
        }
        this.checkedRoleList = JSON.stringify(this.checkedRoleList);
    }

    addUser() {
        var rolObj =  Object.assign([], JSON.parse(this.checkedRoleList));
        rolObj.forEach(element => {
            delete element['id'];
            delete element['isSelected'];
        });
        this.currentUserAdded.roles = rolObj;
        this.userService.addUser(this.currentUserAdded).pipe(first()).subscribe(resp => {
            this.router.navigate(['/users']);
        });
    }

}