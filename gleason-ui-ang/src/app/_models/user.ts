export class User {
    userId: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    email: string;
    token?: string;
    roles: any;
    customer: string;
    isTrailUser: boolean;
}