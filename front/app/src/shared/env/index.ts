import { z } from 'zod'

export const envSchema = z.object({
  BACKEND_API: z.string().url(),
})

export const getEnv = () => {
  return envSchema.parse({
    BACKEND_API: 'http://localhost:5075/',
  })
}
