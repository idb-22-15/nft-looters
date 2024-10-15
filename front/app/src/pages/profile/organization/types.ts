export type Recommendation = {
  id: string
  issuer: {
    id: string
    firstName: string
    lastName: string
    avatar?: string
  }
  title: string
  text: string
}
