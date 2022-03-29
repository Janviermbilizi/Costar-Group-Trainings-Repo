import { Photo } from './photo';
export interface Member {
  id: number;
  fullName: string;
  email: string;
  memberId: string;
  age: number;
  dateOfBirth: Date;
  created: Date;
  lastActive: Date;
  gender: string;
  introduction: string;
  interests: string;
  city: string;
  country: string;
  photos: Photo[];
}
