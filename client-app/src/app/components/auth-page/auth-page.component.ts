import { AfterViewInit, ChangeDetectorRef, Component, OnDestroy, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CompackToastService, TypeToast } from 'ngx-compack';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.scss']
})
export class AuthPageComponent implements OnInit, AfterViewInit, OnDestroy {

  // typeView
  @ViewChild('loginView') loginView: TemplateRef<any>;
  @ViewChild('registerView') registerView: TemplateRef<any>;
  public nowView: TemplateRef<any>;
  private isLogIn = true;
  // field login/pass
  public userName: string | undefined;
  public password: string | undefined;
  public email: string | undefined;
  // http
  private subAuth: Subscription | null | undefined;
  public authIsChecking = false;

  constructor(
    private cdr: ChangeDetectorRef,
    private route: Router,
    private cts: CompackToastService,
    private authService: AuthService) { }

  ngOnInit() { }

  ngAfterViewInit() {
    this.nowView = this.loginView;
    this.cdr.detectChanges();
  }

  public loadViewTemplate() {
    this.nowView = this.isLogIn ? this.registerView : this.loginView;
    this.isLogIn = !this.isLogIn;
    this.cdr.detectChanges();
  }

  ngOnDestroy() {
    if (this.subAuth != null) {
      this.subAuth.unsubscribe();
      this.subAuth = null;
    }
  }

  public logIn() {
    if (this.userName && this.password && !this.authIsChecking) {
      this.authIsChecking = true;
      this.subAuth = this.authService.logIn(this.userName, this.password)
        .subscribe(
          (data) => {
            if (data) {
              if (data.role == 'admin')
                this.route.navigate(['/admin']);
              else
                this.route.navigate(['/user']);
              this.cts.emitNotife(TypeToast.Success, 'Успешная авторизация');
            }
            else
              this.cts.emitNotife(TypeToast.Error, 'Неверный log/pass');
          },
          error => {
            this.cts.emitNotife(TypeToast.Error, error.message);
            this.authIsChecking = false;
          }
        );
    }
  }

  public register() {
    if (this.email && this.userName && this.password && !this.authIsChecking) {
      this.authIsChecking = true;
      this.subAuth = this.authService.register(this.userName, this.password, this.email)
        .subscribe(
          (data) => {
            if (data) {
              if (data.role == 'admin')
                this.route.navigate(['/admin']);
              else
                this.route.navigate(['/user']);
              this.cts.emitNotife(TypeToast.Success, 'Успешная авторизация');
            }
            else
              this.cts.emitNotife(TypeToast.Error, 'Неверный log/pass');
          },
          error => {
            this.cts.emitNotife(TypeToast.Error, error.error?.message ?? 'ошибка');
            this.authIsChecking = false;
          }
        );
    }

  }

}
