import { Address } from "../../../shared/models/address.model";
import { ClubCocktail } from "./club-cocktail.model";
import { ClubMember } from "./club-member.model";
import { ClubRole } from "./club-role.model";

export interface Club {
  id: string;
  address: Address;
  cocktails: ClubCocktail[];
  description: string;
  name: string;
  members: ClubMember[];
  roles: ClubRole[];
  visibility: string;
}
