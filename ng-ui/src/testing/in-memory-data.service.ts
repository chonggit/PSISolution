import { InMemoryDbService, RequestInfo } from 'angular-in-memory-web-api';

export class InMemoryDataService implements InMemoryDbService {

    createDb(reqInfo?: RequestInfo) {

        const roles: Role[] = [
            { Id: 1, Name: 'Administrators' }
        ];

        return { roles };
    }

    genId(roles: Role[]): number {
        return roles.length > 0 ? roles.length + 1 : 0;
    }
}