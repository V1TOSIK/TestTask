document.addEventListener("alpine:init", () => {
  Alpine.data("bookingModal", () => ({
        bookingForm: { },

        close() {
            Alpine.store('globalState').closeBookingModal();
            this.bookingForm = { };
        },

        getActiveTab(){
          return Alpine.store('globalState').activeTab;  
        },

        async submit() {
            try {
                const store = Alpine.store('globalState');
                const token = store.currentUser.token;
                if (!token) {
                    alert("⛔ You must be logged in to make a booking!");
                return;
                }

                this.bookingForm.RoomId = store.bookingRoomId;

                if (!this.bookingForm.RoomId) return;

                if (!store.currentUser.token) {
                    alert("Please log in for booking.");
                    return;
                }

                this.bookingForm.UserId = store.currentUser.id;

                if (!this.bookingForm.CheckInDate || !this.bookingForm.CheckOutDate) {
                    alert("Please confirm date.");
                    return;
                }

                const res = await fetch("http://localhost:8000/api/bookings", {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    body: JSON.stringify(this.bookingForm)
                });

                if (!res.ok) throw new Error(await res.text());
                const data = await res.json();

                if (data.IsFailure) {
                    alert(data.Error);
                } else {
                    alert('Booking added successfully!');
                }

                await store.fetchBookings();

                this.close();
            } catch (err) {
                alert('Error: ' + err.message);
            }
        },
    }));
});