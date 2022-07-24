import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'div[app-login]',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup<{ userName: FormControl<string | null>, password: FormControl<string | null> }>;

  onSubmit(): void {
    if (this.loginForm.valid) {
      console.log('submit', this.loginForm.value);
      this.router.navigate(['/']);
    } else {
      Object.values(this.loginForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  constructor(private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      userName: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(20)]),
      password: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(20)]),
    });
  }
}
