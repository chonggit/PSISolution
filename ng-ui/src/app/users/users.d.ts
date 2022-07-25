interface User {
  Id?: number;
  UserName?: string;
  NormalizedUserName?: string;
  Email?: string;
  NormalizedEmail?: string;
  EmailConfirmed?: boolean;
  PasswordHash?: string;
  SecurityStamp?: string;
  ConcurrencyStamp?: string;
  PhoneNumber?: string;
  PhoneNumberConfirmed?: boolean;
  TwoFactorEnabled?: boolean;
  LockoutEnd?: Date;
  LockoutEnabled?: boolean;
  AccessFailedCount?: number;
}
