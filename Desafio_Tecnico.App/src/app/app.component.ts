import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';
import { MenuComponent } from './navegacao/menu/menu.component';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MenuComponent, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Desafio_Tecnico.App';
  showMenu = true;

  constructor(private router: Router) {
    this.updateMenuVisibility(this.router.url);

    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event) => {
        this.updateMenuVisibility((event as NavigationEnd).urlAfterRedirects);
      });
  }

  private updateMenuVisibility(url: string) {
    this.showMenu = !['/login', '/user-novo'].includes(url);
  }
}
