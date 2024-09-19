import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthWithApiService } from '../../../services/auth-with-api.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(
        /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
      ),
    ]),
  });

  constructor(
    private formbuilder: FormBuilder,
    private router: Router,
    private authService: AuthWithApiService
  ) {}

  get getEmail() {
    return this.loginForm.controls['email'];
  }
  get getPassword() {
    return this.loginForm.controls['password'];
  }

  // Submit the login form
  loginSubmit() {
    if (this.loginForm.valid) {
      console.log('Form Submitted:', this.loginForm.value);
      const credentials = {
        email: this.getEmail.value!,
        password: this.getPassword.value!,
      };

      // Call the login function from AuthWithApiService
      this.authService.login(credentials).subscribe({
        next: (response) => {
          // Handle success
          if (response.success) {
            alert('Login Succefully');
            this.router.navigate(['/home']);
          } else {
            alert('Invalid email or password');
          }
        },
        error: (error) => {
          // Handle error
          console.error('Login error', error);
          alert('Something went wrong! Please try again.');
        },
        complete: () => {
          // Handle completion (optional)
          console.log('Login request completed');
        },
      });
    }
  }

  // loginSubmit() {
  //   const { email, password } = this.loginForm.value;
  //   if (this.loginForm.valid) {
  //     this.authService.getByEmail(email as string).subscribe({
  //       next: () => {
  //         alert('Login Successfully');
  //         this.loginForm.reset();
  //         this.router.navigate(['home']);
  //       },
  //     });
  //   } else {
  //     alert('Something went wrong');
  //   }
  // }
}
