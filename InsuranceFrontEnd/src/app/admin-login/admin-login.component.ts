import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ValidateForm } from '../helper/validateForm';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent {




  loading = false; // Initialize loading flag
  token: any = '';

  constructor(private auth: AuthService, private router: Router) { }

  loginForm = new FormGroup({
    userName: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  jwtHelper = new JwtHelperService();

  loginUser() {
    this.loading = true; // Set loading to true when the login process starts

    if (this.loginForm.valid) {
      this.auth.login(this.loginForm.value).subscribe({
        next: (data) => {
          this.token = data;
          const decodedToken = this.jwtHelper.decodeToken(this.token.actualToken);
          const role: string = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
          if (role != null) {

            if (role === "Admin") {
              alert("Login Successfully");
              localStorage.setItem("token", this.token.actualToken);
              localStorage.setItem("username", this.loginForm.get('userName')?.value!);
              localStorage.setItem("password", this.loginForm.get('password')?.value!);
              this.router.navigateByUrl('/admin');
            }
            if (role === "Employee") {
              alert("Login Successfully");
              localStorage.setItem("token", this.token.actualToken);
              localStorage.setItem("username", this.loginForm.get('userName')?.value!);
              localStorage.setItem("password", this.loginForm.get('password')?.value!);
              this.router.navigateByUrl('/employee/header');
            }
            if (role === "Agent") {
              alert("Login Successfully");
              localStorage.setItem("token", this.token.actualToken);
              localStorage.setItem("username", this.loginForm.get('userName')?.value!);
              localStorage.setItem("password", this.loginForm.get('password')?.value!);
              this.router.navigateByUrl('/agent/header');
            }
            if (role === "Customer") {
              alert("Login Successfully");
              localStorage.setItem("token", this.token.actualToken);
              localStorage.setItem("username", this.loginForm.get('userName')?.value!);
              localStorage.setItem("password", this.loginForm.get('password')?.value!);
              this.router.navigateByUrl('/customer/header');
            }
          } else {
            alert("User Not Found")
          }
        },
        error: (error: HttpErrorResponse) => {
          alert("wrong Username & Password");
          console.log(error);
        },
        complete: () => {
          // After the login operation is complete, set loading back to false to hide the spinner
          this.loading = false;
        }
      });
    } else {
      ValidateForm.validateAllFormFileds(this.loginForm);
      alert("One or more fields are required");
      this.loading = false; // Set loading to false in case of form validation error
    }
  }
}
