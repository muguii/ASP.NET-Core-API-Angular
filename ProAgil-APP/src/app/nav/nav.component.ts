import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private toastr: ToastrService, public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  loggedIn() {
    return this.authService.LoggedIn();
  }

  entrar() {
    this.router.navigate(['/user/login']);
  }

  logout() {
    localStorage.removeItem('token');
    this.toastr.show('Logout');
    this.router.navigate(['/user/login']);
  }
}
