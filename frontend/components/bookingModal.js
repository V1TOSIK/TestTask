function bookingModal() {
    return {
        bookingForm: { },

        open() {
            Alpine.store('globalState').bookingModalOpen = true;
        },

        close() {
            Alpine.store('globalState').bookingModalOpen = false;
            this.bookingForm = { };
        },

        getActiveTab(){
          return Alpine.store('globalState').activeTab;  
        },

        async submit() {
            try {
                const store = Alpine.store('globalState');
                const token = store.currentUser.token;

                if (!store.currentUser.token) {
                    alert("Please log in for booking.");
                    return;
                }

                if (!bookingForm.bookingCheckIn || !bookingForm.bookingCheckOut) {
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
    }
}