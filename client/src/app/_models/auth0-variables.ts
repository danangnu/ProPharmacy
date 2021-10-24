interface AuthConfig {
    clientID: string;
    domain: string;
    callbackURL: string;
    apiUrl: string;
  }
  
  export const AUTH_CONFIG: AuthConfig = {
    clientID: '',
    domain: '',
    callbackURL: 'http://localhost:4200/callback',
    apiUrl: ''
  };