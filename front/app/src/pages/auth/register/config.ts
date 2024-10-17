import { z } from 'zod'

import { validationErrors } from '~/src/shared/config'

export const userSchema = z.object({
  firstName: z.string({ message: validationErrors.required }).min(2, validationErrors.minChars(2)).max(256, validationErrors.maxChars(256)),
  lastName: z.string({ message: validationErrors.required }).min(2, validationErrors.minChars(2)).max(256, validationErrors.maxChars(256)),
  email: z.string({ message: validationErrors.required }).email(),
  password: z
    .string({ message: validationErrors.required })
    .min(6, { message: validationErrors.minChars(6) })
    .max(256, { message: validationErrors.maxChars(256) }),
  repeatPassword: z
    .string({ message: validationErrors.required })
    .min(6, { message: validationErrors.minChars(6) })
    .max(256, { message: validationErrors.maxChars(256) }),
}).refine(v => v.repeatPassword === v.password, { path: ['repeatPassword'], message: 'Пароли не совпадают' })

export const organizationSchema = z.object({
  name: z.string({ message: validationErrors.required }).min(2, validationErrors.minChars(2)).max(256, validationErrors.maxChars(256)),
  email: z.string({ message: validationErrors.required }).email(),
  password: z
    .string({ message: validationErrors.required })
    .min(6, { message: validationErrors.minChars(6) })
    .max(256, { message: validationErrors.maxChars(256) }),
  repeatPassword: z
    .string({ message: validationErrors.required })
    .min(6, { message: validationErrors.minChars(6) })
    .max(256, { message: validationErrors.maxChars(256) }),
}).refine(v => v.repeatPassword === v.password, { path: ['repeatPassword'], message: 'Пароли не совпадают' })
