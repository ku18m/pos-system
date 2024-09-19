import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Route, Router, RouterModule } from '@angular/router';
import { AuthWithApiService } from '../../../services/auth-with-api.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(
        /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
      ),
    ]),
    rememberMe: new FormControl(false),
  });

  constructor(
    private router: Router,
    private authService: AuthWithApiService
  ) {}

  ngOnInit(): void {}

  get getUserName() {
    return this.loginForm.controls['username'];
  }
  get getPassword() {
    return this.loginForm.controls['password'];
  }
  get getRememberMe() {
    return this.loginForm.controls['rememberMe'];
  }

  loginSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched(); // Ensures all fields are marked if the form is invalid
      return;
    }

    //const { email, password, rememberMe } = this.loginForm.value;
    const username = this.loginForm.get('username')?.value as string;
    const password = this.loginForm.get('password')?.value as string;
    const rememberMe = this.loginForm.get('rememberMe')?.value as boolean;

    this.authService.login({ username, password, rememberMe }).subscribe({
      next: (response) => {
        if (response.success) {
          this.router.navigate(['/home']);
        } else {
          this.handleError('Invalid username or password');
        }
      },
      error: (error) => {
        this.handleError('Login failed. Please try again later.', error);
      },
    });
  }

  // Handle errors from the API
  private handleError(message: string, error?: any): void {
    console.error(message, error?.message);
    alert(message);
  }

  // Getters for form controls
  get emailControl() {
    return this.loginForm.get('username');
  }

  get passwordControl() {
    return this.loginForm.get('password');
  }

  get rememberMeControl() {
    return this.loginForm.get('rememberMe');
  }
}
