import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

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
      userName: [null, [Validators.required, Validators.maxLength(20)]],
      password: [null, [Validators.required], Validators.maxLength(20)],
    });
  }
}
