import { Injectable } from '@angular/core';

const THEME_KEY = 'app-theme';
const DARK_CLASS = 'app-dark';

@Injectable({
  providedIn: 'root'
})
export class LayoutService {
  private darkMode = false;

  constructor() {
    const storedTheme = localStorage.getItem(THEME_KEY);
    this.darkMode = storedTheme === 'dark' || (
      !storedTheme && window.matchMedia('(prefers-color-scheme: dark)').matches
    );
    this.applyTheme();
  }

  toggleDarkMode(): void {
    this.darkMode = !this.darkMode;
    this.applyTheme();
    localStorage.setItem(THEME_KEY, this.darkMode ? 'dark' : 'light');
  }

  isDarkMode(): boolean {
    return this.darkMode;
  }

  private applyTheme(): void {
    const htmlEl = document.documentElement;
    htmlEl.classList.toggle(DARK_CLASS, this.darkMode);
  }
}
