import { z } from 'zod'

export const envSchema = z.object({
  BACKEND_API: z.string().url(),
})

export const getEnv = () => {
  console.log(process.env)
  return envSchema.parse(process.env)
}
