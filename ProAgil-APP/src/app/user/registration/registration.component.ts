import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/User';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup | any;
  user: User;

  constructor(public formBuilder: FormBuilder, private toastr: ToastrService, private authService: AuthService, private router: Router) {
    this.user = new User();
  }

  ngOnInit(): void {
    this.validation();
  }

  validation() {
    this.registerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [ Validators.required, Validators.email ]],
      userName: ['', Validators.required],
      passwords: this.formBuilder.group({
        password: ['', [ Validators.required, Validators.minLength(4) ]],
        confirmPassword: ['', Validators.required]
      }, { validator: this.compararSenhas })
    });
  }

  compararSenhas(formGroup: FormGroup) {
    const password = formGroup.get('password');
    const confirmPassword = formGroup.get('confirmPassword');

    if (confirmPassword?.errors == null || 'mismatch' in confirmPassword?.errors) {
      if (password?.value !== confirmPassword?.value) {
        confirmPassword?.setErrors({ mismatch: true });
      }
      else {
        confirmPassword?.setErrors(null);
      }
    }
  }

  cadastrarUsuario() {
    if (this.registerForm.valid) {
      this.user = Object.assign(
        { password: this.registerForm.get('passwords.password').value },
        this.registerForm.value
      );

      this.authService.Register(this.user).subscribe(
        () => {
          this.router.navigate(['/user/login']);
          this.toastr.success('Cadastro realizado');
        },
        error => {
          const erros = error.error;
          erros.forEach((erro: { code: any; }) => {
            switch (erro.code) {
              case 'DuplicateUserName':
                this.toastr.error('Usuario já existe.');
                break;
              default:
                this.toastr.error(`Erro no cadastro. ErroCode: ${erro.code}`);
            }
          });
        }
      );
    }
  }
}
