export type Certificate = {
  id: string
  title: string
  issuedDate: Date
  issuerId: string
  verified: boolean
  image?: {
    url: string
  }
}

export type Issuer = {
  id: string
  name: string
  certificateTypes: string[]
}
