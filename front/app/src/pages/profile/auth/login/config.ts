import { z } from 'zod'

import { validationErrors } from '~/src/shared/config'

export const loginSchema = z.object({
  email: z.string({ message: validationErrors.required }).email({ message: 'Должен быть валидный email' }),
  password: z.string({ message: validationErrors.required }).min(6, validationErrors.minChars(6)).max(256, validationErrors.maxChars(256)),
})
