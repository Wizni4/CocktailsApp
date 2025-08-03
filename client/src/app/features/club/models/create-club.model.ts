import { Address } from "../../../shared/models/address.model";

export interface CreateClubRequest {
  address: Address,
  name: string,
  description: string,
  visibility: string,
}
