import { Injectable } from '@angular/core';

const THEME_KEY = 'app-theme'; // 'dark' | 'light'
const DARK_CLASS = 'my-app-dark';

@Injectable({ providedIn: 'root' })
export class LayoutService {
  private darkMode = false;

  constructor() {
    const stored = localStorage.getItem(THEME_KEY);
    const prefersDark = window.matchMedia?.('(prefers-color-scheme: dark)').matches;

    this.darkMode = stored ? stored === 'dark' : prefersDark;
    this.apply();
  }

  toggleDarkMode(): void {
    this.darkMode = !this.darkMode;
    localStorage.setItem(THEME_KEY, this.darkMode ? 'dark' : 'light');
    this.apply();
  }

  isDarkMode(): boolean {
    return this.darkMode;
  }

  private apply() {
    // put the class on <html> (or <body>) so it wraps the whole app
    document.documentElement.classList.toggle(DARK_CLASS, this.darkMode);
  }
}
