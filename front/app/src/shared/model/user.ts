import { mockOrganization, mockUser } from '../__mocks__'
import { backendApi } from '../api'

export type Gender = 'male' | 'female'

export type UserProfile = {
  id: string
  avatar?: string
  firstName: string
  lastName: string
  email: string
  gender?: Gender
  birthday?: string
  organization?: OrganizationProfile
}

export type CreateUserProfile = {
  avatar?: string
  firstName: string
  lastName: string
  email: string
  password: string
  repeatPassword: string
}

export type OrganizationProfile = {
  id: string
  avatar?: string
  name: string
  email: string
  employees: Array<Omit<UserProfile, 'organization'>>
}

export type CreateOrganizationProfile = {
  avatar?: string
  name: string
  email: string
  password: string
  repeatPassword: string
}

export type UpdateOrganizationProfile = {
  avatar?: string
  name: string
}

export const isUserProfile = (profile: UserProfile | OrganizationProfile): profile is UserProfile => {
  return !('employees' in profile)
}

export const isOrganizationProfile = (profile: UserProfile | OrganizationProfile): profile is OrganizationProfile => {
  return ('employees' in profile)
}

export const useProfileStore = defineStore('profile', () => {
  const { data: profile, status, error, refresh: load }
    = useAsyncData('profile', async () => {
      // backendApi<UserProfile | OrganizationProfile>('/profile')

      // throw new Error('ooo')
      return new Promise((res) => {
        setTimeout(() => res(mockOrganization), 1000)
      }) as Promise<UserProfile | OrganizationProfile>
    })

  const isLoggedIn = computed(() => profile.value !== undefined)
  const maybeUser = computed(() => profile.value ? isUserProfile(profile.value) ? profile.value : null : undefined)
  const maybeOrganization = computed(() => profile.value ? isOrganizationProfile(profile.value) ? profile.value : null : undefined)

  watch(profile, () => {
    if (profile.value === null) navigateTo('/auth/login')
  })

  const isUserProfile_ = computed(() => {
    if (profile.value === undefined) return null
    return isUserProfile(profile.value)
  })

  const isOrganizationProfile_ = computed(() => {
    if (profile.value === undefined) return null
    return isOrganizationProfile(profile.value)
  })

  return {
    profile,
    status,
    error,
    isLoggedIn,
    maybeUser,
    maybeOrganization,
    isUserProfile: isUserProfile_,
    isOrganizationProfile: isOrganizationProfile_,
    load,
  }
})

export const useUserStore = defineStore('user', () => {
  const profileStore = useProfileStore()
  const { profile, status, error } = storeToRefs(profileStore)

  // const user = computed<UserProfile | undefined>({
  //   get: () => {
  //     if (isUserProfile.value === false) throw new Error('profile is not user')
  //     return profile.value as UserProfile | undefined
  //   },
  //   set: value => profile.value = value,
  // })

  const login = async (data: { password: string }) => {
    profile.value = await backendApi('/auth/user/login', { body: data })
  }

  const register = async (data: CreateUserProfile) => {
    profile.value = await backendApi('/auth/user/register', { body: data })
  }

  const logout = async () => {
    await backendApi('/auth/user/logout')
    profile.value = undefined
  }

  const exitOrganization = async () => {
    await backendApi('/user/exitOrganization');
    (profile.value as UserProfile).organization = undefined
  }

  const update = async (data: UserProfile) => {
    // await backendApi('/user', {method: "PUT", body: data})
    profile.value = data
  }

  return {
    status, error, update, login, register, logout, exitOrganization,
  }
})

export const useAuthenticatedUser = () => {
  const { profile, isUserProfile } = storeToRefs(useProfileStore())

  return computed(() => {
    if (!profile.value || !isUserProfile.value) throw new Error('Пользователь должен быть аутентифицирован')
    return profile.value as UserProfile
  })
}

export const useOrganizationStore = defineStore('organization', () => {
  const profileStore = useProfileStore()
  const { profile, status, error } = storeToRefs(profileStore)

  // const organization = computed<OrganizationProfile | undefined>({
  //   get: () => {
  //     if (isUserProfile.value === false) throw new Error('profile is not organization')
  //     return profile.value as OrganizationProfile | undefined
  //   },
  //   set: value => profile.value = value,
  // })

  // const update = async (newUser: OrganizationProfile) => {
  //   organization.value = newUser
  // }

  const login = async (data: { password: string }) => {
    profile.value = await backendApi('/auth/organization/login', { body: data })
  }

  const register = async (data: CreateOrganizationProfile) => {
    profile.value = await backendApi('/auth/organization/register', { body: data })
  }

  const logout = async () => {
    await backendApi('/auth/user/logout')
    profile.value = undefined
  }

  const update = async (data: UpdateOrganizationProfile) => {
    // await backendApi('/organization', { method: 'PUT', body: data })
    if (profile.value)
      profile.value = { ...profile.value, ...data }
  }

  return {
    status, error, login, register, logout, update,
  }
})

export const useAuthenticatedOrganization = () => {
  const { profile, isOrganizationProfile } = storeToRefs(useProfileStore())

  return computed(() => {
    if (!profile.value || !isOrganizationProfile.value) throw new Error('Организация должна быть аутентифицирована')
    return profile.value as OrganizationProfile
  })
}
