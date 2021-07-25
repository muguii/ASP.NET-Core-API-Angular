import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo: string;
  model: any;

  constructor(private toastr: ToastrService, private authService: AuthService, public router: Router) {
    this.titulo = 'Login';
    this.model = {};
  }

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      this.router.navigate(['/dashboard']);
    }
  }

  login() {
    this.authService.Login(this.model).subscribe(
      () => {
        this.router.navigate(['/dashboard']);
        this.toastr.success('Logado com sucesso');
      },
      error => {
        this.toastr.error('Falha ao logar');
      }
    );
  }
}
