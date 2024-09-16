import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink,HttpClient,Router],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private http: HttpClient, private router: Router) {}  

  onSubmit(form: any) {  
    // const credentials = {  
    //   username: form.value.username,  
    //   password: form.value.password  
    // };  

    // this.http.post<{ token: string }>('', credentials).subscribe({  
    //   next: (response) => {  
    //     localStorage.setItem('token', response.token);  
    //     this.router.navigate(['/home']);  
    //   },  
    //   error: (error) => {  
    //     console.error('Login error', error);  
    //     alert('Login failed! Please try again.');  
    //   }  
    // });  
  }  
}
