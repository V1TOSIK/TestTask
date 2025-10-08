document.addEventListener('alpine:init', () => {
  Alpine.store('globalState', {
    loginModalOpen: false,
    addModalOpen: false,
    updateModalOpen: false,
    bookingModalOpen: false,

    activeTab: 'hotels',
    filteredCities: [],
    filteredDate: '',

    bookingRoomId: null,
    bookingCheckIn: '',
    bookingCheckOut: '',


    hotels: [],
    rooms: [],
    bookings: [],

    hotelsPagination: { page: 1, pageSize: 24, total: 0 },
    roomsPagination: { page: 1, pageSize: 24, total: 0 },
    bookingsPagination: { page: 1, pageSize: 24, total: 0 },

    currentUser: {
        id: '',
        role: '',
        token: ''
    },

    openBookingModal(roomId) {
        this.bookingRoomId = roomId;
        this.bookingCheckIn = '';
        this.bookingCheckOut = '';
        this.bookingModalOpen = true;
    },

    closeBookingModal() {
        this.bookingModalOpen = false;
        this.bookingRoomId = null;
        this.bookingCheckIn = '';
        this.bookingCheckOut = '';
    },

    setActiveTab(tabName){
        this.activeTab = tabName,
        this.filterCity = [],
        this.filterDate = ''
    },

    checkUserRole(role){
      return this.currentUser.role === role; 
    },

    deleteItem(type, item) {
      const token = this.currentUser.token;
      const id = item.id;
      const url = type === 'hotels'
          ? `http://localhost:8000/api/hotels/${id}`
          : `http://localhost:8000/api/rooms/${id}`;

      if (!confirm(`Are you sure you want to delete this ${type.slice(0, -1)}?`)) return;

      fetch(url, {
          method: 'DELETE',
          headers: {
              'Authorization': `Bearer ${token}`
          }
      })
      .then(async res => {
          if (!res.ok) throw new Error(await res.text());
          alert('Deleted successfully!');
          // Після видалення оновлюємо список
          if (type === 'hotels') this.fetchHotels();
          else this.fetchRooms();
      })
      .catch(err => {
          alert('Error: ' + err.message);
      });
    },

     async fetchHotels() {
          try {
              const { page, pageSize } = this.hotelsPagination;

              const selectedCities = this.filteredCities && this.filteredCities.length > 0 ? this.filteredCities : [];

              const query = this.buildQueryParams({
                  pageNumber: page,
                  pageSize,
                  cities: selectedCities 
              });

              const res = await fetch(`http://localhost:8000/api/hotels/${query}`);
              const data = await res.json();

              this.hotels = data.value.items;
              this.hotelsPagination.total = data.value.totalCount;
          } catch (err) {
              console.error('Помилка при завантаженні готелів:', err);
          }
      },

          async fetchRooms() {
          try {
              const { page, pageSize } = this.roomsPagination;

              const selectedCities = this.filteredCities && this.filteredCities.length > 0 ? this.filteredCities : [];


              const query = this.buildQueryParams({
                  pageNumber: page,
                  pageSize,
                  cities: selectedCities,
                  checkInDate: this.filteredDate
              });

              const res = await fetch(`http://localhost:8000/api/rooms${query}`);
              const data = await res.json();

              this.rooms = data.value.items;
              this.roomsPagination.total = data.value.totalCount;
          } catch (err) {
              console.error('Помилка при завантаженні кімнат:', err);
          }
      },

    async fetchBookings() {
        try {
            if (!this.currentUser.token || !['Admin', 'User'].includes(this.currentUser.role)) {
                this.bookings = [];
                this.bookingsPagination.total = 0;
                return;
            }

            const { page, pageSize } = this.bookingsPagination;
            const url = this.currentUser.role === 'Admin'
                ? `http://localhost:8000/api/bookings?pageNumber=${page}&pageSize=${pageSize}`
                : `http://localhost:8000/api/bookings/${this.currentUser.id}?pageNumber=${page}&pageSize=${pageSize}`;

            const res = await fetch(url, {
                headers: { 'Authorization': `Bearer ${this.currentUser.token}` }
            });

            if (!res.ok) throw new Error(await res.text());

            const data = await res.json();
            this.bookings = data.value.items;
            this.bookingsPagination.total = data.value.totalCount;

        } catch (err) {
            console.error('Помилка при завантаженні бронювань:', err);
            this.bookings = [];
            this.bookingsPagination.total = 0;
        }
    },

    buildQueryParams(params) {
        const query = Object.entries(params)
            .filter(([_, value]) => value !== '' && value !== null && value !== undefined)
            .map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`)
            .join('&');
        return query ? `?${query}` : '';
    },

    setPage(type, page) {
        if (type === 'hotels') this.hotelsPagination.page = page;
        else if (type === 'rooms') this.roomsPagination.page = page;
        else if (type === 'bookings') this.bookingsPagination.page = page;

        if (type === 'hotels') this.fetchHotels();
        else if (type === 'rooms') this.fetchRooms();
        else if (type === 'bookings') this.fetchBookings();
    },

    totalPages(type) {
        if (type === 'hotels') return Math.ceil(this.hotelsPagination.total / this.hotelsPagination.pageSize);
        if (type === 'rooms') return Math.ceil(this.roomsPagination.total / this.roomsPagination.pageSize);
        if (type === 'bookings') return Math.ceil(this.bookingsPagination.total / this.bookingsPagination.pageSize);
        return 0;
    }
  });
});
