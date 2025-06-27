import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: '/',
			name: 'home',
			component: () => import('../views/SignIn.vue'),
		},
		{
			path: '/events',
			name: 'events',
			component: () => import('@/views/Events/EventListView.vue'),
		},
		{
			path: '/events/:eventId/matches',
			name: 'matches',
			component: () => import('@/views/Matches/MatchListView.vue'),
		},
		{
			path: '/match/:matchId/edit',
			name: 'matchDetails',
			component: () => import('@/views/Matches/MatchDetailsView.vue'),
		},
		{
			path: '/teams',
			name: 'teams',
			component: () => import('@/views/Teams/TeamListView.vue'),
		},
	],
})

export default router
