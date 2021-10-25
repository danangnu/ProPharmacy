export interface AuthData {
    client_id: string;
    client_secret: string;
    audience: string;
    grant_type: string;
  }

export const AUTH_CONFIG: AuthData = {
    client_id : '3LAzpux32P5mdOUDumFq8FuudQ07V3Cu',
    client_secret : 'QC0SIC1Wp0TCDb_4_VPIqbJriCe68VyAFR1JcqxTcsWzBTodn31GnwPZEJzd581f',
    audience : 'https://api.propharmacy',
    grant_type : 'client_credentials',
};
