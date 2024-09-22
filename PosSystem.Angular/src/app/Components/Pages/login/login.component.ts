import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthWithAPIService } from '../../../services/auth-with-api.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UsersWithAPIService } from '../../../services/users-with-api.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  userName: any;
  password: any;
  loginError: any;
  token: any;

  constructor(private fb: FormBuilder, private authService: AuthWithAPIService, private router: Router, private userService: UsersWithAPIService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }


  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.login(this.userName, this.password).subscribe({
        next: response => {
          // Assuming the token is returned in the response  
          const token = response.token; // Adjust this based on your API response  
          localStorage.setItem('token', token); // Store the token in local storage
          this.router.navigate(['/home']);
        },
        error: (error) => {
          this.loginError = "Invalid UserName or Password";
        }
      });

      // this.token = localStorage.getItem('token');
      // console.log(this.token);
      // this.userService.fetchAllUsers(this.token).subscribe({
      //   next: (response: any) => {
      //     for (var i = 0; i < response.length; i++) {
      //       if (response[i].userName == this.userName) {
      //         this.loginError = null;
      //         if (this.password == "password123") {
      //           this.loginError = null;
      //           this.router.navigate(['/home']); // Redirect after successful login 
      //         }
      //       }
      //     }
      //   }
      // });
    }
  }
}
