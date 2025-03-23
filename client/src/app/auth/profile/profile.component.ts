import container from '../../../di-container';
import { Component, OnInit } from '@angular/core';
import { IUser, IAuthService } from '../auth.service';
import { ApiService } from '../../api/api.service'
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  standalone: false,
})
export class ProfileComponent implements OnInit {
  private authService: IAuthService = container.resolve<IAuthService>('AuthService');
  loading: boolean = false;
  user: IUser = {} as IUser;
  test: any;

  constructor(private apiService: ApiService) { }

  public async ngOnInit(): Promise<void> {
    this.authService.getUser()
      .then((user: any) => {
        this.user.name = user.attributes["loginid"];
      });
    var token = await this.authService.getJwtToken();
    const data = await this.apiService
      .withHeaders({"Authorization": `Bearer ${token}`})
      .get("user");
    this.test = JSON.stringify(data); // Convert the data to a string
    console.log(this.test);
  }

  public update(): void {
    this.loading = true;

    this.authService.updateUser(this.user)
      .then(() => {
        this.loading = false;
      }).catch(() => {
        this.loading = false;
      });
  }

}
