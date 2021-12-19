import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';

export class UserDto {
  public fName: string;
  public sName: string;
  public userName: string;
  public email: string;
  public phone: string;
}

export class SubscriptionDto {
  public id: number;
  public userName: string;
  public system: string;
  public theme: string;
  public name: string;
  public email: string;
  public phone: string;
  public type: string;
}

export class NotifictiosDto {
  public date: string;
  public message: string;
  public system: string;
  public theme: string;
  public isSended: string;
}

export class SystemDto {
  public id: number;
  public systemName: string;
  public themes: ThemeDto[];
}

export class ThemeDto {
  public id: number;
  public themeName: string;
}

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  public systemsData: SystemDto[] = [];
  // public themesData: ThemeDto[] = [];
  public notifData: NotifictiosDto[] = [];
  public subscribleData: SubscriptionDto[] = [];
  public usersData: UserDto[] = [];

  public selectedSystemToViewTheme: SystemDto = null;

  // flags
  public servicesLoading = true;
  public themesLoading = true;
  public notificationsLoading = true;
  public subscribersLoading = true;
  public usersLoading = true;

  constructor(
    private title: Title,
    public authSerice: AuthService,
    private apiService: ApiService) { }

  ngOnInit() {
    this.title.setTitle('admin');
    this.getSystems();
    this.getNotofocations();
    this.getSubscriptions();
    this.getUsers();
  }

  private getSystems() {
    this.servicesLoading = true;
    this.themesLoading = true;
    this.systemsData = [];
    this.selectedSystemToViewTheme = null;
    this.apiService.get('dictionary')
      .subscribe(
        (next: SystemDto[]) => {
          this.systemsData = next;
          if (this.selectedSystemToViewTheme)
            for (let syst of this.systemsData) {
              if (this.selectedSystemToViewTheme.id == syst.id)
                this.selectedSystemToViewTheme = syst;
            }
        },
        error => { },
        () => {
          this.servicesLoading = false;
          this.themesLoading = false;
        }
      );
  }

  public getNotofocations() {
    this.notificationsLoading = true;
    this.notifData = [];
    this.apiService.get('notification')
      .subscribe(
        (next: NotifictiosDto[]) => {
          this.notifData = next;
        },
        error => { },
        () => this.notificationsLoading = false
      );
  }

  public getSubscriptions() {
    this.subscribersLoading = true;
    this.subscribleData = [];
    this.apiService.get('subscription')
      .subscribe(
        (next: SubscriptionDto[]) => {
          this.subscribleData = next;
        },
        error => { },
        () => this.subscribersLoading = false
      );
  }

  public getUsers() {
    this.usersLoading = true;
    this.usersData = [];
    this.apiService.get('user')
      .subscribe(
        (next: UserDto[]) => {
          this.usersData = next;
        },
        error => { },
        () => this.usersLoading = false
      );
  }

  public newSystemName: string = '';
  public addSystem() {
    if (this.newSystemName) {
      const httpParam = new HttpParams()
        .append('name', this.newSystemName);
      this.apiService.get('dictionary/sys-add', httpParam)
        .subscribe(() => {
          this.getSystems();
        });
    }
  }
  public newThememNme: string = '';
  public addTheme() {
    if (this.newThememNme && this.selectedSystemToViewTheme) {
      const httpParam = new HttpParams()
        .append('systemId', this.selectedSystemToViewTheme.id.toString())
        .append('name', this.newThememNme);
      this.apiService.get('dictionary/them-add', httpParam)
        .subscribe(() => {
          this.getSystems();
        });
    }
  }

  public removeSystem(id: number) {
    if (window.confirm('точно?')) {
      const httpParam = new HttpParams()
        .append('id', id.toString());
      this.apiService.get('dictionary/sys-rm', httpParam)
        .subscribe(() => {
          this.getSystems();
          if (this.selectedSystemToViewTheme.id == id)
            this.selectedSystemToViewTheme = null;
        });
    }
  }
  public removeTheme(id: number) {
    if (window.confirm('точно?') && this.selectedSystemToViewTheme) {
      const httpParam = new HttpParams()
        .append('systemId', this.selectedSystemToViewTheme.id.toString())
        .append('themeId', id.toString());
      this.apiService.get('dictionary/them-rm', httpParam)
        .subscribe(() => {
          this.getSystems();
        });
    }
  }

  public updateSystem(system: SystemDto) {
    if (window.confirm('точно?')) {
      const httpParam = new HttpParams()
        .append('id', system.id.toString())
        .append('name', system.systemName);
      this.apiService.get('dictionary/sys-up', httpParam)
        .subscribe(() => {
          this.getSystems();
        });
    }
  }
  public updateTheme(theme: ThemeDto) {
    if (window.confirm('точно?') && this.selectedSystemToViewTheme) {
      const httpParam = new HttpParams()
        .append('systemId', this.selectedSystemToViewTheme.id.toString())
        .append('themeId', theme.id.toString())
        .append('name', theme.themeName);
      this.apiService.get('dictionary/them-up', httpParam)
        .subscribe(() => {
          this.getSystems();
        });
    }
  }

  public logOut() {
    this.authSerice.logOut();
  }

}
