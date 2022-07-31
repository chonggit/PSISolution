
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

export abstract class RoleEditBaseComponent {

    roleForm!: FormGroup<{ Id: FormControl<number | null>, Name: FormControl<string | null>, ConcurrencyStamp: FormControl<string | null> }>;
    abstract title: string;
    abstract onSubmit(): void;

    constructor(private fb: FormBuilder) { }

    onInit(): void {
        this.roleForm = this.fb.group({
            Id: new FormControl<number | null>(null,),
            Name: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(50)]),
            ConcurrencyStamp: new FormControl<string | null>(null)
        });
    }
}