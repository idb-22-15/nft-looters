import { mockUser } from '../__mocks__'
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

const isUserProfile = (profile: UserProfile | OrganizationProfile): profile is UserProfile => {
  return !('employees' in profile)
}

const isOrganizationProfile = (profile: UserProfile | OrganizationProfile): profile is OrganizationProfile => {
  return ('employees' in profile)
}

export const useProfileStore = defineStore('profile', () => {
  const { data: profile, status, error, refresh: load }
    = useAsyncData('profile', async () => {
      // backendApi<UserProfile | OrganizationProfile>('/profile')
      return new Promise((res) => {
        setTimeout(() => res(mockUser), 1000)
      }) as Promise<UserProfile | OrganizationProfile>
    })

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
    profile, status, error, isUserProfile: isUserProfile_, isOrganizationProfile: isOrganizationProfile_, load,
  }
})

export const useUserStore = defineStore('user', () => {
  const profileStore = useProfileStore()
  const { profile, status, error, isUserProfile } = storeToRefs(profileStore)

  const user = computed<UserProfile | undefined>({
    get: () => {
      if (isUserProfile.value === false) throw new Error('profile is not user')
      return profile.value as UserProfile | undefined
    },
    set: value => profile.value = value,
  })

  const update = async (newUser: UserProfile) => {
    user.value = newUser
  }

  const login = async (data: { password: string }) => {
    user.value = await backendApi('/auth/user/login', { body: data })
  }

  const register = async (data: CreateUserProfile) => {
    user.value = await backendApi('/auth/user/register', { body: data })
  }

  const logout = async () => {
    await backendApi('/auth/user/logout')
    user.value = undefined
  }

  return {
    user, status, error, update, login, register, logout,
  }
})

export const useAuthenticatedUser = () => {
  const { user } = storeToRefs(useUserStore())

  return computed(() => {
    if (!user.value) throw new Error('Пользователь должен быть аутентифицирован')
    return user.value
  })
}

export const useOrganizationStore = defineStore('organization', () => {
  const profileStore = useProfileStore()
  const { profile, status, error, isUserProfile } = storeToRefs(profileStore)

  const organization = computed<OrganizationProfile | undefined>({
    get: () => {
      if (isUserProfile.value === false) throw new Error('profile is not organization')
      return profile.value as OrganizationProfile | undefined
    },
    set: value => profile.value = value,
  })

  // const update = async (newUser: OrganizationProfile) => {
  //   organization.value = newUser
  // }

  const login = async (data: { password: string }) => {
    organization.value = await backendApi('/auth/organization/login', { body: data })
  }

  const register = async (data: CreateOrganizationProfile) => {
    organization.value = await backendApi('/auth/organization/register', { body: data })
  }

  const logout = async () => {
    await backendApi('/auth/user/logout')
    organization.value = undefined
  }

  return {
    organization, status, error, login, register, logout,
  }
})

export const useAuthenticatedOrganization = () => {
  const { organization } = storeToRefs(useOrganizationStore())

  return computed(() => {
    if (!organization.value) throw new Error('Организация должна быть аутентифицирована')
    return organization.value
  })
}
