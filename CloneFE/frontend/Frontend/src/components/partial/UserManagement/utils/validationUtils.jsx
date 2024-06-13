export function isValidPhoneNumber(phoneNumber) {
  const phoneRegex = /^0\d{9}$/;
  return phoneRegex.test(phoneNumber);
}

export function isValidName(name) {
  const nameRegex = /^[A-Za-z\s\u00C0-\u1EF9]+$/;
  return nameRegex.test(name);
}

export function calculateAge(dateOfBirth) {
  const dob = new Date(dateOfBirth);
  const now = new Date();
  const diffMilliseconds = now - dob;
  const age = Math.floor(diffMilliseconds / (1000 * 60 * 60 * 24 * 365.25));
  return age;
}

export function formatPhoneNumber(phone) {
  const digits = phone.replace(/\D/g, '');
  const match = digits.match(/^(\d{0,4})(\d{0,3})(\d{0,3})$/);
  if (match) {
    return match.slice(1).filter(Boolean).join(' ');
  }
  return phone;
}

