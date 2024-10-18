import type { UserProfile } from '~/src/shared/model/user'

export type Diploma = {
  id: string
  title: string
  issuedDate: Date
  issuedUser: UserProfile
  image?: {
    url: string
  }
}
