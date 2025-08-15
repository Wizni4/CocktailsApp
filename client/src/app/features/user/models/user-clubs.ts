export interface UserClubs {
  userId: string;
  clubs: UserClubItem[];
}

export interface UserClubItem {
  clubId: string;
  clubName: string;
  clubDescription: string;
  isOwner: boolean;
  imageUrl:string;
  roleNames: string[];
  effectivePermissions: string[];
}
