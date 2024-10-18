import type { UserProfile } from '~/src/shared/model/user'

export type Certificate = {
  id: string
  title: string
  issuedDate: Date
  issuedUser: UserProfile
  image?: {
    url: string
  }
}
