import { InMemoryDbService } from 'angular-in-memory-web-api';

export class InMemoryService implements InMemoryDbService {
    createDb() {
        let roles: Role[] = [
            { Id: 1, Name: 'Administrators' }
        ];
        return { roles };
    }
}