import 'vue-router'
import type { SecurityRole } from '@/api'

export {}

declare module 'vue-router' {
	interface RouteMeta {
		role: SecurityRole
	}
}
