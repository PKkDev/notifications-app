import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';
import { NotifictiosDto, SubscriptionDto, SystemDto, ThemeDto } from '../admin/admin.component';

export class AddSubscriptionQuery {
  constructor(public system: string, public systemId: number, public theme: string, public themeId: number, public type: TypeSubscription) { }
}

export enum TypeSubscription {
  Mail = 0,
  Sms = 1,
  Telegram = 2
}

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  public systemsData: SystemDto[] = [];
  public selectedSystem: SystemDto = null;
  public selectedTheme: ThemeDto = null;
  public selectedType: TypeSubscription = TypeSubscription.Mail;

  public notifData: NotifictiosDto[] = [];
  public subscribleData: SubscriptionDto[] = [];

  public isStartAddSubs = false;

  // flags
  public notificationsLoading = true;
  public subscribersLoading = true;

  constructor(
    private title: Title,
    public authSerice: AuthService,
    private apiService: ApiService) { }

  ngOnInit() {
    this.title.setTitle('user');
    this.getNotofocations();
    this.getSubscriptions();
  }

  public getNotofocations() {
    this.notificationsLoading = true;
    this.notifData = [];
    const id = this.authSerice.getUserId();
    const httpParam = new HttpParams()
      .append('id', id.toString())
    this.apiService.get('notification/user', httpParam)
      .subscribe(
        (next: NotifictiosDto[]) => {
          this.notifData = next;
        },
        error => { },
        () => {
          this.notificationsLoading = false;
        });
  }

  public getSubscriptions() {
    this.subscribersLoading = true;
    this.subscribleData = [];
    const id = this.authSerice.getUserId();
    const httpParam = new HttpParams()
      .append('id', id.toString())
    this.apiService.get('subscription/user', httpParam)
      .subscribe(
        (next: SubscriptionDto[]) => {
          this.subscribleData = next;
        },
        error => { },
        () => {
          this.subscribersLoading = false;
        });
  }

  public removeSubscriptions(id: number) {
    if (window.confirm('точно?')) {
      const httpParam = new HttpParams()
        .append('id', id.toString())
      this.apiService.get('subscription/rm', httpParam)
        .subscribe(() => {
          this.getSubscriptions();
        });
    }
  }

  public startAddSubs() {
    this.isStartAddSubs = !this.isStartAddSubs;
    this.getSystems();
  }

  private getSystems() {
    this.apiService.get('dictionary')
      .subscribe((next: SystemDto[]) => {
        this.systemsData = next;
        if (next.length > 0) {
          this.selectedSystem = next[0];
          if (next[0].themes.length > 0)
            this.selectedTheme = next[0].themes[0];
        }
        this.selectedType = TypeSubscription.Mail;
      });
  }

  public addSubscription() {
    if (this.selectedSystem, this.selectedTheme) {
      const id = this.authSerice.getUserId();
      const httpParam = new HttpParams()
        .append('id', id.toString())
      const body = new AddSubscriptionQuery(this.selectedSystem.systemName, this.selectedSystem.id, this.selectedTheme.themeName,
        this.selectedTheme.id, (+this.selectedType));
      this.apiService.post('subscription/add', body, httpParam)
        .subscribe(() => {
          this.getSubscriptions();
          this.isStartAddSubs = !this.isStartAddSubs;
        });
    }
  }

  public logOut() {
    this.authSerice.logOut();
  }

}
