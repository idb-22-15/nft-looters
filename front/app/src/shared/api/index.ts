import { getEnv } from '../env'

export const backendApi = $fetch.create({ baseURL: 'localhost:3000' })
