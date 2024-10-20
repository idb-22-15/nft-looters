import { getEnv } from '../env'

export const backendApi = $fetch.create({ baseURL: getEnv().BACKEND_API })
